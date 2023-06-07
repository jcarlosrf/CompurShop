using CompurShop.Domain.Entities;
using CompurShop.Domain.Interfaces;
using System;
using System.Collections;
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
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                return _context.SaveChanges();
            }

            return 0;
        }


        public IQueryable<Usuario> GetAll()
        {
            var query =  _context.Usuarios.Include("Cliente").Where(u=> u.IdCliente > 0);

            var query2 = _context.Usuarios.Where(u => u.IdCliente == 0);
            
            var retorno = query.Concat(query2).OrderBy(u => u.Nome);

            return retorno;
        }

        public IQueryable<Usuario> GetbyNome(string nome)
        {
            var query = GetAll();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(u => u.Nome.ToUpper().Contains(nome.ToUpper()) || u.Cliente.Nome.ToUpper().Contains(nome.ToUpper())); 

            return query;
        }
    }
}
