﻿using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CompurShop.Domain.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public int Gravar(Usuario usuario)
        {
            usuario.IdNivel = usuario.IdCliente == 0 ? 1 : 2;
            usuario.Senha = Criptografia.CriptografiaSenha(usuario.Senha.Trim());

            return _usuarioRepository.Save(usuario);
        }

        public int Apagar(int idusuario)
        {
            return _usuarioRepository.Delete(idusuario);
        }

        public Usuario GetEntidade(int idusuario)
        {
            return _usuarioRepository.GetEntity(idusuario);
        }

        public Usuario GetPermissao(string nome, string senha)
        {
            senha = Criptografia.CriptografiaSenha(senha.Trim());

            Usuario user = _usuarioRepository.GetByName(nome);

            if (user == null)
                return null;            

            return user.Senha.Equals(senha) ? user : null;            
        }

        public IEnumerable<Usuario> GetbyNome(string nome)
        {
            var users = _usuarioRepository.GetbyNome(nome).ToList();
            return users;
        }
    }
}
