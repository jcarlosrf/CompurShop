﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Scire.Arquivos
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            
            if (args.Length > 0 && args[0] == "-c")
            {
                BotArquivos meuBot = new BotArquivos();
                meuBot.Executar();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new ProcessScireArquivos()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
