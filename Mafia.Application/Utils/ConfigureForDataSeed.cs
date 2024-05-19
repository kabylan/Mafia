using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mafia.Utils
{
    public class ConfigureForDataSeed
    {
    }

    /// <summary>
    /// Тип классов для импорта в Data Seed
    /// </summary>
    public enum TypeClassForSeed
    {
        /// <summary>
        /// Страна
        /// </summary>
        Country,
        /// <summary>
        /// Области
        /// </summary>
        Oblast,
        /// <summary>
        /// Район
        /// </summary>
        Raion,
        /// <summary>
        /// Лпу, организации
        /// </summary>
        Lpu,
        /// <summary>
        /// МКБ Диагнозы
        /// </summary>
        MKBDiagnose
    }

    public enum FileTypeForSeed
    {
        /// <summary>
        /// Для Excel файлов
        /// </summary>
        XLS,
        /// <summary>
        /// Для Csv файлов
        /// </summary>
        CSV,
    }
}
