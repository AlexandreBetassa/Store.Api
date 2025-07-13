using Microsoft.Extensions.Options;
using Project.CrossCutting.Configurations.v1;
using Project.CrossCutting.Exceptions;
using Project.Framework.Core.v1.Bases.CommandHandler;
using Store.Domain.Models.v1.Cache;

namespace Store.Application.Commands.v1.Auth.GenerateToken
{
    public class GenerateTokenCommandHandler : BaseCommandHandler<GenerateTokenCommand, GenerateTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRedisService _redisService;
        private readonly IPasswordServices _passwordServices;
        private readonly Appsettings _appsettingsConfiguration;

        public GenerateTokenCommandHandler(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IUserRepository userRepository,
            IRedisService redisService,
            IPasswordServices passwordServices,
            IOptions<Appsettings> appsettingsConfigurations,
            IHttpContextAccessor contextAccessor)
                : base(loggerFactory.CreateLogger<GenerateTokenCommandHandler>(), mapper, contextAccessor)
        {
            _userRepository = userRepository;
            _redisService = redisService;
            _passwordServices = passwordServices;
            _appsettingsConfiguration = appsettingsConfigurations.Value;
        }

        public override async Task<GenerateTokenResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation("Inicio {handler}.{method}", nameof(GenerateTokenCommandHandler), nameof(Handle));

                var user = await _userRepository.GetByEmailOrUsernameAsync(request.Email)
                    ?? throw new NotFoundException("Usuário não encontrado");

                var isValidPassword = _passwordServices.VerifyPassword(user.Login, user.Login.Password, request.Password);

                if (!isValidPassword)
                    throw new UnauthorizedException(HttpStatusCode.Unauthorized, "Usuário ou senha inválidos");

                var token = GenerateToken(user);

                await _redisService.CreateAsync(token,
                    new RedisTokenModel(token, user.Name.First, user.Login.Email, user.Role.ToString()));

                Logger.LogInformation("Fim {handler}.{method}", nameof(GenerateTokenCommandHandler), nameof(Handle));

                return new()
                {
                    Token = token
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "{handler}.{method}", nameof(GenerateTokenCommandHandler), nameof(Handle));

                throw;
            }
        }

        private string GenerateToken(UserAccount user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appsettingsConfiguration.JwtConfiguration.SecretJwtKey);

            var credentials = new SigningCredentials(
                                                    new SymmetricSecurityKey(key),
                                                    SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appsettingsConfiguration.JwtConfiguration.Issuer,
                Audience = _appsettingsConfiguration.JwtConfiguration.Audience,
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(_appsettingsConfiguration.JwtConfiguration.ExpirationInMinutes),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private ClaimsIdentity GenerateClaims(UserAccount user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("id", user.Id.ToString()));
            ci.AddClaim(new Claim("name", user.Name.First));
            ci.AddClaim(new Claim("email", user.Login.Email));
            ci.AddClaim(new Claim("role", user.Role.ToString()));

            return ci;
        }
    }
}