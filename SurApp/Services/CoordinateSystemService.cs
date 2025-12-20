using System.IO;
using System.Text.Json;
using SurApp.Models;

namespace SurApp.Services;

public class CoordinateSystemService {

    /// <summary>
    /// Stores the given items into a file on disc
    /// </summary>
    /// <param name="CoordinateSystem">Coordinate System</param>
    public static async Task SaveToFileAsync(CoordinateSystem coordinateSystem, string jsonFileName) {
        // Ensure all directories exists
        Directory.CreateDirectory(Path.GetDirectoryName(jsonFileName)!);

        // We use a FileStream to write all items to disc
        using (var fs = File.Create(jsonFileName)) {
            await JsonSerializer.SerializeAsync(fs, coordinateSystem);
        }
    }

    /// <summary>
    /// Loads the file from disc and returns the items stored inside
    /// </summary>
    /// <returns>An IEnumerable of items loaded or null in case the file was not found</returns>
    public static async Task<CoordinateSystem?> LoadFromFileAsync(string jsonFileName) {
        try {
            // We try to read the saved file and return the CoordinateSystem if successful
            using var fs = File.OpenRead(jsonFileName);
            return await JsonSerializer.DeserializeAsync<CoordinateSystem>(fs);
        } catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException) {
            // In case the file was not found, we simply return null
            return null;
        }
    }
}
