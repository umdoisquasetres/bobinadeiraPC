namespace bobinadeiraPC
{
    public static class CommandConstants
    {
        // =================================================================
        // Comandos enviados do PC para o Microcontrolador
        // =================================================================

        /// <summary>
        /// Comando para iniciar o processo de bobinagem.
        /// </summary>
        public const string StartWinding = "CMD:START\n";

        /// <summary>
        /// Comando para parar o processo de bobinagem imediatamente.
        /// </summary>
        public const string StopWinding = "CMD:STOP\n";

        /// <summary>
        /// Comando para resetar a contagem de voltas no microcontrolador.
        /// </summary>
        public const string ResetWinding = "CMD:RESET\n";

        /// <summary>
        /// Formato do comando de configuração.
        /// Use com string.Format(ConfigFormat, espiras, rpm, diametro).
        /// Ex: CFG:E100;R800;D0.50
        /// </summary>
        public const string ConfigFormat = "CFG:E{0};R{1};D{2:0.00}\n";


        // =================================================================
        // Prefixo de mensagens recebidas do Microcontrolador
        // =================================================================

        /// <summary>
        /// Prefixo para mensagens de progresso.
        /// Ex: PROG:50 (indicando 50% concluído)
        /// </summary>
        public const string ProgressPrefix = "PROG:";
    }
}

