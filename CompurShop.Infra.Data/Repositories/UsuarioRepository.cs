using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompurShop.Infra.Data.Repositories
{
    public class UsuarioRepository :IUsuarioRepository
    {
        private readonly ScireDbContext _context;

        public UsuarioRepository(ScireDbContext context)
        {
            _context = context;
        }

        public int Save(Usuario usuario)
        {
            if (usuario.Id == 0)
            {
                _context.Usuarios.Add(usuario);
            }
            else
            {
                var existingCliente = _context.Usuarios.Find(usuario.Id);
                if (existingCliente != null)
                {
                    existingCliente.Nome = usuario.Nome;
                    existingCliente.Senha = usuario.Senha;
                    existingCliente.IdCliente = usuario.IdCliente;
                    existingCliente.IdNivel = usuario.IdNivel;
                }
            }

           return _context.SaveChanges();
        }

        public Usuario GetEntity(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario GetByName(string nome)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nome.Equals(nome));

            return usuario;
        }

        public int Delete(int id)
        {
            var usuario = _context.Clientes.Find(id);
            if (usuario != null)
            {
                _context.Clientes.Remove(usuario);
                return _context.SaveChanges();
            }

            return 0;
        }
    }
}
