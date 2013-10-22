using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using banco_de_dados_local.Model;

namespace UFABC_Power_CR
{
    public class CR
    {
        
        double creditosTotais=0, somaProdutos=0;
        public double valor;
        public CR(List<banco_de_dados_local.Model.Historico> hist)
        {
            
            foreach (banco_de_dados_local.Model.Historico materia in hist)
            {
                switch (materia.Conceito)
                {
                    case "A":
                        somaProdutos += (4 * materia.Credito);
                        creditosTotais += materia.Credito;
                        break;
                    case "B":
                        somaProdutos += (3 * materia.Credito);
                        creditosTotais += materia.Credito;
                        break;
                    case "C":
                        somaProdutos += (2 * materia.Credito);
                        creditosTotais += materia.Credito;
                        break;
                    case "D":
                        somaProdutos += materia.Credito;
                        creditosTotais += materia.Credito;
                        break;
                    case "F":
                        creditosTotais += materia.Credito;
                        break;
                    case "O":
                        creditosTotais += materia.Credito;
                        break;
                    default:
                        break;
                }
                
            }

            if (creditosTotais != 0)
            {
                valor = Math.Round(somaProdutos / creditosTotais, 3);

            }
            
        }

        public void salvaCA()
        {
            App.db.Aluno.First().CA = valor;
            App.db.SubmitChanges();
        }

        public void salvaCR(int quadrimestre)
        {
            if (quadrimestre == 0)
            {
                App.db.Aluno.First().CR = valor;
            }
            else
            {
                banco_de_dados_local.ViewModel.ToDoViewModel a = new banco_de_dados_local.ViewModel.ToDoViewModel();
                var queryQ = from banco_de_dados_local.Model.Quadrimestre quadri in App.db.Quadrimestres where quadri.Numero == quadrimestre select quadri;
                foreach (banco_de_dados_local.Model.Quadrimestre quadri in queryQ)
                {
                    quadri.CR = valor;
                }
            }
            App.db.SubmitChanges();
        }

        public string imprime()
        {
            return Math.Round(valor, 3).ToString();
        }


    }
}
