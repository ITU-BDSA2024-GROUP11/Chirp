using System.Globalization;
using CsvHelper;

namespace SimpleDB;

public class CsvDatabase<T> : IDatabaseRepository<T>
{
    public IEnumerable<T> Read(int ? limit = null)
    {
        using (var reader = new StreamReader("./chirp_cli_db.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Reads the records directly into the Cheep record
            return csv.GetRecords<T>().ToList();
        }
    }

    public void Store(T record)
    {
        using (var writer = new StreamWriter("./chirp_cli_db.csv", append: true))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            writer.WriteLine();
            csv.WriteRecord(record);
        }
    }
}