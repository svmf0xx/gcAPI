﻿using gcapi.Enums;
using System.Drawing;

namespace gcapi.Interfaces
{
    public class IEventStyle
    {
        public EventColor Color { get; set; } = EventColor.White; //блин, Color не хочет сохраняться в базе напрямую, пришлось поменять на енум


        public string Emoji { get; set; } = "🎉"; // хмм
                                                  //а зачем? Это типа настройки внешнего вида, которые вообще могут храниться в куки, а не в бд
                                                  //плюс тут не должна храниться аватарка пользователя
                                                  //это не аватарка, и вообще я щас всё поменяю тут понял

        //упростить жизнь себе и все остальным - сдеалть эмоджи
        //в тыщу раз безопаснее, чем поддержвать загрузку файлов на сервер

        //ну и плюс нет нужды хранить это в отдельной таблице бд, это просто маленькая структура данных, как свойство ивента, пусть и будет храниться с ним
        //ну надо подумать жеско над аватаркой - samfoxx, 14:38, 14.10.2024

        //Короче, для аутентификации на сайте будет использоваться OTP (one-time-pasword) через условный Google Authентификатор (хотя на самом деле
        //несколько приложений этот стандарт поддерживают)
        //В базе для этого достаточно просто хранить СуПеР сЕкРеТнУю строку, которую сделать при регистрации пользователя
        //когда дойдем до реализации регистрации на сайте, могу сделать эту часть
        //Для тг такой карусели не нужно, там можно считать, что пользователь, который пишет боту, заведомо аутентифицирован - alexlender, 18:04, 14.10.2024

        //ээээээ ладно чето я жеско затупил в том как разграничить ивенты и планы
        //типа делать всё в одном сервисе слишком запутанно
        //а если 2 разных для ивентов и планов то смысл вообще в интерфейсе для них
        //и короче пока подожду с сервисом для каловых объектов - samfoxx, 22:03, 14.10.2024

        //зачем в апи отсылать эксепшены, это же капец неудобно, лучше в ответ бэдреквест с указанием ошибки
        //ну и плюс все методы, которые возвращают просто Task или Task<bool>, лучше переделать в Task<IActionResult> хотя бы
        //это же рест всё-таки, хттп, всё такое - alexlender, 17:20, 16.10.2024

        //ну с путами ты конечно сильно придумал
        //это будет самое каноникал рест апи - alexlender, 7:44, 17.10.2024

        //гад дээм - alexlender, 14:32, 17.10.2024

    }

}
