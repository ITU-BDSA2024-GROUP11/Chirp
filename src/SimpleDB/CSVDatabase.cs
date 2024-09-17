using System.Globalization;
using CsvHelper;

namespace SimpleDB;

public class CsvDatabase<T> : IDatabaseRepository<T>
{
    private static CsvDatabase<T> _instance = null;

    private CsvDatabase() { }

    public static CsvDatabase<T> getInstance() {
        if(_instance == null) _instance = new CsvDatabase<T>();
        return _instance;
    }


    const string path = "../../data/chirp_cli_db.csv";
    public IEnumerable<T> Read(int ? limit = null)
    {
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Reads the records directly into the Cheep record
            return csv.GetRecords<T>().ToList();
        }
    }

    public void Store(T record)
    {
        using (var writer = new StreamWriter(path, append: true))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            writer.WriteLine();
            csv.WriteRecord(record);
        }
    }
}