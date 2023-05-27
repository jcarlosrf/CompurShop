using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Scire.Arquivos
{
    public partial class ProcessScireArquivos : ServiceBase
    {
        private Timer timer;

        public ProcessScireArquivos()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {           

            timer = new Timer(30000);
            timer.Elapsed += TimerElapsed;
            timer.AutoReset = true;
            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            BotArquivos meuBot = new BotArquivos();
            meuBot.Executar();
        }

        protected override void OnStop()
        {
            // Pare o timer ao parar o serviço
            timer.Stop();
            timer.Dispose();
        }
    }
}
