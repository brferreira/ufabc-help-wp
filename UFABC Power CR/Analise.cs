using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UFABC_Power_CR
{
    public  class Analise
    {

        
        //SINGLETON
        private static Analise instance;
        private Analise() { }

        public static Analise Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Analise();
                }
                return instance;
            }
        }

        private static string analiseCP(double CP, int quadriAtual, double CA)
        {
            if (quadriAtual <= 3)
            {
                if (CP <= 0.3)
                {
                    return "";
                }
                else
                {
                    return App.aluno.Nome.Split(' ')[0].Split(' ')[0] + ", seu CP está excelente! ";
                };
            }
            else if (quadriAtual > 3 && quadriAtual <= 6)
            {
                if (CP <= 0.2)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", seu CP está muito baixo, se matricule em mais matérias! ";
                }
                else if (CP > 0.2 && CP <= 0.32)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", você precisa subir seu CP, se matricule em mais matérias! ";
                }
                else if (CP > 0.32 && CP <= 0.8)
                {
                    return "";
                }
                else if (CP > 0.8 && CP < 1)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", seu CP está excelente, você está perto do fim do curso! ";
                }
                else if (CP >= 1 && CA >= 2)
                {
                    return "Parabéns " + App.aluno.Nome.Split(' ')[0] + ", você se formou! :)";
                }
            }

            else if (quadriAtual > 6 && quadriAtual <= 8)
            {
                if (CP <= 0.32)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", seu CP está muito baixo, você corre risco de ser jubilado! Se matricule em mais matérias. ";
                }
                else if (CP > 0.32 && CP <= 0.44)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", você precisa subir seu CP, se matricule em mais matérias. ";
                }
                else if (CP > 0.44 && CP <= 0.8)
                {
                    return "";
                }
                else if (CP > 0.8 && CP < 1)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", seu CP está ótimo, você está perto do fim do curso! ";
                }
                else if (CP >= 1 && CA >= 2)
                {
                    return "Parabéns " + App.aluno.Nome.Split(' ')[0] + ", você se formou! :)";
                }
            }

            else if (quadriAtual > 8 && quadriAtual <= 12)
            {
                if (CP <= 0.44)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", seu CP está muito baixo! Matricule-se em mais matérias. ";
                }
                else if (CP > 0.44 && CP <= 0.6)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", você precisa subir seu CP, se matricule em mais matérias. ";
                }
                else if (CP > 0.8 && CP < 1)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", você está perto do fim do curso! ";
                }
                else if (CP >= 1 && CA >= 2)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", parabéns, você se formou! :)";
                }
            }

            else if (quadriAtual > 12 && quadriAtual <= 15)
            {
                if (CP <= 0.5)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", seu CP está muito baixo! Matricule-se em mais matérias. ";
                }
                else if (CP > 0.5 && CP <= 0.8)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", você precisa subir seu CP, se matricule em mais matérias. ";
                }
                else if (CP > 0.8 && CP < 1)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", você está perto do fim do curso! ";
                }
                else if (CP >= 1 && CA >= 2)
                {
                    return "Parabéns" + App.aluno.Nome.Split(' ')[0] + ", você se formou! :)";
                }
            }

            else if (quadriAtual > 15 && quadriAtual <= 18)
            {
                if (CP <= 0.5)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", seu CP está em nível crítico! Matricule-se em mais matérias. ";
                }
                else if (CP > 0.5 && CP <= 0.8)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", seu CP está muito baixo! Matricule-se em mais matérias. ";
                }
                else if (CP > 0.8 && CP < 1)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", você está perto do fim do curso! ";
                }
                else if (CP >= 1 && CA >= 2)
                {
                    return "Parabéns " + App.aluno.Nome.Split(' ')[0] + ", você se formou! :)";
                }
            }
            else
            {
                if (CP <= 0.8)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", o seu tempo para se formar está acabando! Matricule-se no maior número de matérias possível! ";
                }
                else if (CP > 0.8 && CP < 1)
                {
                    return App.aluno.Nome.Split(' ')[0] + ", você está perto do fim do curso, mas o tempo para você se formar é curto! ";
                }
                else if (CP >= 1 && CA >= 2)
                {
                    return "Parabéns" + App.aluno.Nome.Split(' ')[0] + ", você se formou! :)";
                }
               
            }
            return "";
        }


        private static string analiseCRCA(double CR, double CA, double CP)
        {
            if (CP < 1)
            {
                if (CR <= 1)
                {
                    if (CA < 2)
                    {
                        return "Esforce-se para subir seu CR e CA! Refaça matérias que reprovou.";
                    }
                    else if (CA > 2)
                    {
                        return "Esforce-se para subir seu CR.";
                    }
                }
                else if (CR > 1 && CR < 2)
                {
                    if (CA < 2)
                    {
                        return "Esforce-se para subir seu CR e CA! Refaça matérias que reprovou ou obteve conceito D.";
                    }
                    else
                    {
                        return "Esforce-se para subir seu CR.";
                    }
                }
                else if (CR >= 2 && CR < 3)
                {
                    return "Seu desempenho está bom, continue assim. Considere refazer eventuais matérias que obteve conceito D";
                }
                else
                {
                    return "Seu desempenho está excelente, continue assim!";
                }
            }
            else
            {
                return "";
            }
            return "";   
        }

        private static string gerarAnalise(double CP, int quadriAtual, double CR, double CA)
        {
            if (analiseCP(CP, quadriAtual, CA).Contains(App.aluno.Nome.Split(' ')[0]))
            {
                return analiseCP(CP, quadriAtual, CA) + "\n" + analiseCRCA(CR, CA, CP);
            }
            else
            {
                return App.aluno.Nome.Split(' ')[0] + ", " +  analiseCRCA(CR, CA, CP).ToLower();
            }
        }

        public static void salvarAnalise(double CP, int quadriAtual, double CR, double CA)
        {
            App.db.Aluno.First().Analise =  gerarAnalise(CP, quadriAtual, CR, CA);
            App.db.SubmitChanges();
        }
    }
}
                    
