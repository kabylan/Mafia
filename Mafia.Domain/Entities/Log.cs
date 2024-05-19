using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia.Domain.Entities
{

    /// <summary>
    /// Класс отвечающий за создание логов
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Логин - ПИН
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string FIOUserName { get; set; }
        /// <summary>
        /// Контроллер
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Действие
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Дата создания лога
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// IP пользователя
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// ФИО пациента
        /// </summary>
        public string PatientFIO { get; set; }
        /// <summary>
        /// ID пациента
        /// </summary>
        public int? PatientId { get; set; }
        /// <summary>
        /// Старое значение
        /// </summary>
        public string OldValue { get; set; }
        /// <summary>
        /// Новое значение
        /// </summary>
        public string NewValue { get; set; }
    }

    public static class ActionsForLog
    {
        public static string Created = "Created";
        public static string Updated = "Updated";
        public static string Deleted = "Deleted";
        public static string Find = "Find";
        public static string View = "View";
        public static string Get = "Get";
        public static string GetAll = "GetAll";
    }

    public static class ControllerForLog
    {
        public static string VaccinationCard = "Прививочная карта";
        public static string RefusalVaccinations = "Отмена вакцинации";
        public static string PostscriptHistories = "История приписок";
        public static string Medotvods = "Медвотвод";
        public static string Mantus = "Манту";
        public static string Vaccinations = "Вакцинация";
        public static string PPPIVaccinations = "ПППИ";
        public static string Error = "Error";
    }

    public static class LogsMethods
    {

    }
}
