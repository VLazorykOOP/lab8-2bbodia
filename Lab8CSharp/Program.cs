#region Task1
using System.Text.RegularExpressions;

 string pattern = @"(?=\d+\.\d+\.\d+\.\d+$)(?:(?:25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]|[0-9])\.?){4}$";
using (var reader = new StreamReader("task1.txt"))
{
    using var writer = new StreamWriter("output1.txt");
    string? line;
    while ((line = reader.ReadLine()) != null)
    {
        var matches = Regex.Matches(line, pattern);
        foreach (Match match in matches)
        {
            writer.WriteLine(match.Value);
        }
    }
    
}

List<string> strings = new List<string>();
using (var reader = new StreamReader("output1.txt"))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        strings.Add(line);
    }
}
while (true)
{
    Console.Clear();
    Console.WriteLine("Choose string you would like to modify:");
    for (int i = 0; i < strings.Count; i++)
    {
        Console.WriteLine($"{i+1}. {strings[i]}");
    }
    Console.WriteLine("-1. To Exit");
    int choice = Convert.ToInt32(Console.ReadLine());
    if (choice <= 0)
    {
        break;
    }
    Console.Clear();
    Console.WriteLine("Enter new ip-address:");
    string? newLine = Console.ReadLine();
    if (Regex.IsMatch(newLine, pattern))
    {
        strings[choice - 1] = newLine;
        Console.WriteLine("Line has been modified successfully!");
    }
    else
    {
        Console.WriteLine("Something went wrong! Try again!");
    }
    Console.ReadKey();
}

using (var writer = new StreamWriter("output1.txt"))
{
    foreach (string str in strings)
    {
        writer.WriteLine(str);
    }
}
System.Diagnostics.Process.Start("notepad.exe", "output1.txt");
Console.WriteLine("Task 1 completed");
Console.WriteLine("Press Enter to continue");
Console.ReadLine();
Console.Clear();

#endregion
#region Task 2
using (StreamReader reader1 =File.OpenText("task2.txt"))
{
    List<string?> lines = new List<string?>();
    while (!reader1.EndOfStream)
    {
        string? line = reader1.ReadLine();
        lines.Add(line);
    }
    var words = lines.SelectMany(line =>
                     line.Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '(', ')', '[', ']', '{', '}' },
                                StringSplitOptions.RemoveEmptyEntries))
           .Where(word => word.Length == 5)
           .ToList();
    using (Stream stream = new FileStream("output2.txt", FileMode.Create))
    {
        using (StreamWriter writer = new StreamWriter(stream))
        {
            words.ForEach(word => writer.WriteLine(word));
        }
    }
}
System.Diagnostics.Process.Start("notepad.exe", "task2.txt");
System.Diagnostics.Process.Start("notepad.exe", "output2.txt");
Console.WriteLine("Task 2 completed");
Console.WriteLine("Press Enter to continue");
Console.ReadLine();
Console.Clear();
#endregion
#region Task 3
using (StreamReader reader = File.OpenText("task3.txt"))
{
    {
        List<string?> lines = new List<string?>();
        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            lines.Add(line);
        }
        var words = lines.SelectMany(line =>
                        line.Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '(', ')', '[', ']', '{', '}' },
                                   StringSplitOptions.RemoveEmptyEntries));
        var theLongest = words.Max(word => word.Length);
        var filteredWords = words.Where(w => w.Length != theLongest).ToList();
        using (Stream stream = new FileStream("output3.txt", FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                filteredWords.ForEach(word => writer.WriteLine(word));
            }
        }

    }
}
System.Diagnostics.Process.Start("notepad.exe", "task3.txt");
System.Diagnostics.Process.Start("notepad.exe", "output3.txt");
Console.WriteLine("Task 3 completed");
Console.WriteLine("Press Enter to continue");
Console.ReadLine();
Console.Clear();
#endregion
#region Task 4
FileInfo file3 = new FileInfo("task4.dat");
using (FileStream filestream = file3.Create())
{
    using (BinaryWriter binaryWriter = new BinaryWriter(filestream))
    {
        int n = 15;
        for (int i = 1; i <= n; i++)
        {
            binaryWriter.Write(1.0 / i);
        }
    }
}
using (FileStream fileStream = file3.OpenRead())
{
    using (BinaryReader binaryReader = new BinaryReader(fileStream))
    {
        for (long i = 16; i < fileStream.Length; i += 24)
        {
            fileStream.Seek(i, SeekOrigin.Begin);
            Console.WriteLine(binaryReader.ReadDouble());
        }
    }
}
Console.WriteLine("Task 4 completed");
Console.WriteLine("Press Enter to continue");
Console.ReadLine();
Console.Clear();
#endregion

#region Task5
DirectoryInfo tmp = new DirectoryInfo(@"D:\tmp");
if (tmp.Exists)
{
    tmp.Delete(true);
}
tmp.Create();
var dir1 = tmp.CreateSubdirectory("Bandura1");
var dir2 = tmp.CreateSubdirectory("Bandura2");

FileInfo t1 = new FileInfo(Path.Combine(dir1.FullName, "t1.txt"));
FileInfo t2 = new FileInfo(Path.Combine(dir1.FullName, "t2.txt"));

using (StreamWriter  writer1 = t1.CreateText(),
                    writer2 = t2.CreateText())
{
    writer1.WriteLine("<Шевченко Степан Іванович, 2001> року народження, місце проживання <м. Суми>");
    writer2.WriteLine("<Комар Сергій Федорович, 2000 > року народження, місце проживання <м. Київ>");
}

FileInfo t3 = new FileInfo(Path.Combine(dir2.FullName, "t3.txt"));

using(StreamWriter writer = t3.CreateText())
{
    using (StreamReader reader1 = t1.OpenText(),
                        reader2 = t2.OpenText())
    {
        string? line= reader1.ReadLine();
        writer.WriteLine(line);
        line = reader2.ReadLine();
        writer.WriteLine(line);
        
    }
}

t1.ShowFileInfo();
t2.ShowFileInfo();
t3.ShowFileInfo();


t2.MoveTo(Path.Combine(dir2.FullName, "t2.txt"));
t1.MoveTo(Path.Combine(dir2.FullName, "t1.txt"));

dir2.MoveTo(Path.Combine(tmp.FullName,"All"));
dir1.Delete(true);

dir2.GetFiles().ToList().ForEach(file => file.ShowFileInfo());
#endregion