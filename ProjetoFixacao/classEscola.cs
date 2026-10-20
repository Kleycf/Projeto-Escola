using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoFixacao;

    public class Escola
    {
        private Dictionary<string, Aluno> _alunos;

    public Escola()
        {
        _alunos = new Dictionary<string, Aluno>(StringComparer.OrdinalIgnoreCase);
    }

        public bool AdicionarAluno(string nome,int idade)
        {
            if (_alunos.ContainsKey(nome))
            { 
                     return false;
            }
            else
            {
                _alunos.Add(nome, new Aluno(nome, idade));
                return true;
            }
        }

        public IReadOnlyDictionary<string, Aluno> ObterAlunos()
        {
            return _alunos;
        }
       

    public double MediaAluno(string nome)
        {
            if(_alunos.TryGetValue(nome, out var mediaAluno))
            {
                return mediaAluno.CalcularMedia();
            }
            else
            { 
                return 0;
            }
        }


        public bool AdicionarNotaAluno(string nome,int nota)
        {
            if (_alunos.TryGetValue(nome, out var aluno))
            {
                aluno.AdicionarNota(nota);
                return true;
        }
            else
            {
                return false;
            }
        }


        public Aluno? BuscarAluno(string nome)
        {
            if(_alunos.TryGetValue(nome, out var aluno))
            {
                return aluno;
            }
            else
            {
                return null;
            }
        }



}
