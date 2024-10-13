namespace gcapi.Models
{
    public class EventStyleData
    {
        public string Color { get; set; } = "white";
        public string StylePicture { get; set; } = "/etc/themepics/cucumbers.png"; // хмм
        //а зачем? Это типа настройки внешнего вида, которые вообще могут храниться в куки, а не в бд
        //плюс тут не должна храниться аватарка пользователя
        //это не аватарка, и вообще я щас всё поменяю тут понял
    }
}
