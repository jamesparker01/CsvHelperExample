using CsvHelper;
using CsvHelper.Configuration;
using System.Diagnostics;
using System.Globalization;

var csv = new CsvRepository().ReadCsv();

foreach (var row in csv)
{
    Console.WriteLine($"Id: {row.Id} Name: {row.Name}");
}
Debug.Assert(csv.Length == 1);
Console.WriteLine("Only 1 row should be returned as empty values are filtered");
Console.ReadKey();

public class CsvRepository
{
    public Foo[] ReadCsv()
    {
        using (var reader = new StreamReader("testdata.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<FooMap>();
            return csv.GetRecords<Foo>().Where(x => x.Id != string.Empty && x.Name != string.Empty).ToArray();
        }
    }

}

public record Foo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public sealed class FooMap : ClassMap<Foo>
{
    public FooMap()
    {
        Map(m => m.Id).Name("ColumnA");
        Map(m => m.Name).Name("ColumnD");
    }
}