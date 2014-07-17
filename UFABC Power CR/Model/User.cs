using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;

namespace UFABC_Power_CR.Model
{
    // {level":"1","ra":"11038511","id_campus":"1","email":"brferreira@aluno.ufabc.edu.br","id_periodo":"2","ano_ingresso":"2011"}
    class User
    {
        [DeserializeAs(Name = "novocadastro")]
        public int Cadastrar { get; set; }

        [DeserializeAs(Name = "id_aluno")]
        public int IdAluno { get; set; }

        [DeserializeAs(Name = "id_facebook")]
        public int IdFacebook { get; set; }

        [DeserializeAs(Name = "nome")]
        public string Nome { get; set; }

        [DeserializeAs(Name = "key")]
        public string Token { get; set; }

        [DeserializeAs(Name = "level")]
        public int TipoUsuario { get; set; }

        [DeserializeAs(Name = "ra")]
        public int RA { get; set; }

        [DeserializeAs(Name = "id_campus")]
        public int Campus { get; set; }

        [DeserializeAs(Name = "email")]
        public string Email { get; set; }

        [DeserializeAs(Name = "id_periodo")]
        public int Periodo { get; set; }

        [DeserializeAs(Name = "ano_ingresso")]
        public int AnoIngresso { get; set; }
    }
}
