namespace Examen_PF.Utility;

public class PathDb
{
    public static string GetPath(string DataBase)
    {
        string DbRoute = string.Empty;
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            DbRoute = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbRoute = Path.Combine(DbRoute, DataBase);
        }
        else if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            DbRoute = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DbRoute = Path.Combine(DbRoute, "..", "Library", DataBase);
        }

        return DbRoute;
    }
}