using Autenticacao.Jwt.Application.DTOs.v1;
using Autenticacao.Jwt.Domain.Entities.v1;
using Autenticacao.Jwt.Domain.Interfaces.v1.Repositories;
using Autenticacao.Jwt.Domain.Interfaces.v1.Services;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Store.User.CrossCutting.Configurations.v1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autenticacao.Jwt.Application.Commands.v1.GenerateToken
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, GenerateTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRedisService _redisService;
        private readonly IPasswordServices<User> _passwordServices;
        private readonly AppsettingsConfigurations _appsettingsConfiguration;

        private const string _key = "token_";

        public GenerateTokenCommandHandler(
            IUserRepository userRepository,
            IRedisService redisService,
            IPasswordServices<User> passwordServices,
            AppsettingsConfigurations appsettingsConfiguration)
        {
            _userRepository = userRepository;
            _redisService = redisService;
            _passwordServices = passwordServices;
            _appsettingsConfiguration = appsettingsConfiguration;
        }

        public async Task<GenerateTokenResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            var isValidPassword = _passwordServices.VerifyPassword(user, user.Password, request.Password);

            if (!isValidPassword)
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");

            var token = GenerateToken(user);
            var cacheModel = new RedisUserModel(token, user.Name, user.Email, user.Role);

            await _redisService.CreateAsync(token, cacheModel);

            return new()
            {
                Token = token
            };
        }

        private string GenerateToken(User user)
        {
            // Cria uma instância do JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();

            // Converte nossa chave (string) para um array de bytes
            var key = Encoding.ASCII.GetBytes(_appsettingsConfiguration.JwtConfiguration.SecretJwtKey);

            // Utilizando a chave e encriptando para SHA256
            var credentials = new SigningCredentials(
                                                    new SymmetricSecurityKey(key),
                                                    SecurityAlgorithms.HmacSha256Signature);

            // Gerando token descriptor e informar credenciais, claims e tempo de expiração
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appsettingsConfiguration.JwtConfiguration.Issuer,
                Audience = _appsettingsConfiguration.JwtConfiguration.Audience,
                // Adicionando ao token as regras de acesso e outros dados
                Subject = GenerateClaims(user),
                // Adicionando tempo de expiração no token
                Expires = DateTime.UtcNow.AddMinutes(_appsettingsConfiguration.JwtConfiguration.ExpirationInMinutes),
                // Adicionando credenciais
                SigningCredentials = credentials,
            };

            // Gerando o token com todas as informações
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("name", user.Name));
            ci.AddClaim(new Claim("email", user.Email));
            ci.AddClaim(new Claim("role", user.Role));

            return ci;
        }
    }
}