using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using banco_de_dados_local.Model;

namespace UFABC_Power_CR
{
    public class CP
    {
        public double valor;
        double credLimTotal, credObrigTotal, credObrigAprov, credLimAprov;
        double credLivreTotal=0, credLivreAprov = 0;
       

        public CP(List<banco_de_dados_local.Model.Historico> hist, int curso, int anoIngresso)
        {
            
            switch (curso)
            {
                case 0:
                    if (anoIngresso <= 2008)
                    {
                        credLimTotal = 120;
                        credObrigTotal = 70;
                    }
                    else
                    {
                        credLimTotal = 100;
                        credObrigTotal = 90;
                    }
                    break;
                case 1:
                    credObrigTotal = 72;
                    credLimTotal = 80;
                    credLivreTotal = 38;
                    break;
            }
            foreach (banco_de_dados_local.Model.Historico materia in hist)
            {
                if(materia.Conceito.Equals("A") || materia.Conceito.Equals("B") || materia.Conceito.Equals("C") || materia.Conceito.Equals("D") || materia.Conceito.Equals("E"))
                {
                    switch (materia.Tipo)
                    {
                        case 0:
                            credObrigAprov += materia.Credito;
                            break;
                        case 1:
                            if (credLimAprov < credLimTotal)
                            {
                                credLimAprov += materia.Credito;
                            }
                            else
                            {
                                credLivreAprov += materia.Credito;
                            }
                            break;
                        case 2:   
                            credLivreAprov += materia.Credito; 
                            break;
                    }
                }
            }


            if (credObrigAprov > credObrigTotal)
            {
                credObrigAprov = credObrigTotal;
            }
            if (credLimAprov > credLimTotal)
            {
                credLimAprov = credLimTotal;
            }
            if (credLivreAprov > credLivreTotal)
            {
                credLivreAprov = credLivreTotal;
            }


            valor = Math.Round((credObrigAprov + credLimAprov + credLivreAprov) / (credObrigTotal + credLimTotal + credLivreTotal),3);
            App.db.Aluno.First().CP = valor;
            App.db.SubmitChanges();
        }
        public string imprime()
        {
            return "CP: " + Math.Round(valor, 3).ToString();
        }
    }
}
