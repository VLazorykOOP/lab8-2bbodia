public static class FileInfoExtensions
{
    public static void ShowFileInfo(this FileInfo file)
    {
        file.GetType().GetProperties().ToList().ForEach(p => Console.WriteLine($"{p.Name} = {p.GetValue(file)}"));
        Console.WriteLine();
    } 
}
