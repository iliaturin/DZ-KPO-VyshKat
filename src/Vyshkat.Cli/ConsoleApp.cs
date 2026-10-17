using System.Globalization;
using Vyshkat.Core.Domain;
using Vyshkat.Core.Services;

namespace Vyshkat.Cli;

public sealed class ConsoleApp
{
    private readonly FleetService _fleet;

    public ConsoleApp(FleetService fleet)
    {
        _fleet = fleet;
    }

    public void SeedDemoData()
    {
        _fleet.AddTransport(new Scooter("TR-SC-001", 8, 1.4));
        _fleet.AddTransport(new Bicycle("TR-BI-001", 9));
        _fleet.AddTransport(new ElectricBike("TR-EB-001", 5, 2.2));
        _fleet.AddThing(new Helmet("IT-HL-001"));
        _fleet.AddThing(new ChargingStation("IT-CS-001", 3.0));
    }

    public void Run()
    {
        while (true)
        {
            PrintMenu();

            try
            {
                var command = ReadInt("Выберите действие: ");
                Console.WriteLine();

                switch (command)
                {
                    case 0: return;
                    case 1: AddScooter(); break;
                    case 2: AddBicycle(); break;
                    case 3: AddElectricBike(); break;
                    case 4: AddHelmet(); break;
                    case 5: AddChargingStation(); break;
                    case 6: PrintInventory(); break;
                    case 7: PrintReport(); break;
                    case 8: PrintBeginnerTransport(); break;
                    default: Console.WriteLine("Неизвестная команда."); break;
                }
            }
            catch (EndOfStreamException)
            {
                return;
            }
            catch (ArgumentException error)
            {
                Console.WriteLine($"Некорректные данные: {error.Message}");
            }

            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("ВышКат: учет парка");
        Console.WriteLine("1. Добавить самокат");
        Console.WriteLine("2. Добавить велосипед");
        Console.WriteLine("3. Добавить электровелосипед");
        Console.WriteLine("4. Добавить шлем");
        Console.WriteLine("5. Добавить зарядную станцию");
        Console.WriteLine("6. Показать инвентарь");
        Console.WriteLine("7. Отчет по парку и энергии");
        Console.WriteLine("8. Транспорт для новичков");
        Console.WriteLine("0. Выход");
    }

    private void AddScooter()
    {
        var item = new Scooter(ReadText("Инвентарный номер: "),
            ReadInt("Оценка для новичка (1-10): "), ReadDouble("Расход за сутки, кВт·ч: "));
        PrintTransportResult(_fleet.AddTransport(item));
    }

    private void AddBicycle()
    {
        var item = new Bicycle(ReadText("Инвентарный номер: "),
            ReadInt("Оценка для новичка (1-10): "));
        PrintTransportResult(_fleet.AddTransport(item));
    }

    private void AddElectricBike()
    {
        var item = new ElectricBike(ReadText("Инвентарный номер: "),
            ReadInt("Оценка для новичка (1-10): "), ReadDouble("Расход за сутки, кВт·ч: "));
        PrintTransportResult(_fleet.AddTransport(item));
    }

    private void AddHelmet() =>
        PrintThingResult(_fleet.AddThing(new Helmet(ReadText("Инвентарный номер: "))));

    private void AddChargingStation() =>
        PrintThingResult(_fleet.AddThing(new ChargingStation(
            ReadText("Инвентарный номер: "), ReadDouble("Расход за сутки, кВт·ч: "))));

    private void PrintInventory()
    {
        Console.WriteLine($"Объектов на балансе: {_fleet.ItemCount}");
        foreach (var item in _fleet.InventoryItems)
            Console.WriteLine($"- {item.Name} [{item.InventoryNumber}]");
    }

    private void PrintReport()
    {
        Console.WriteLine($"Единиц транспорта: {_fleet.TransportCount}");
        foreach (var (name, count) in _fleet.TransportCounts())
            Console.WriteLine($"- {name}: {count}");

        Console.WriteLine($"Объектов на балансе: {_fleet.ItemCount}");
        Console.WriteLine($"Расход энергии за сутки: {_fleet.TotalDailyEnergyConsumption():0.##} кВт·ч");
    }

    private void PrintBeginnerTransport()
    {
        var transports = _fleet.BeginnerFriendlyTransports();
        Console.WriteLine($"Транспорт для новичков: {transports.Count}");
        foreach (var transport in transports)
            Console.WriteLine($"- {transport.Name} [{transport.InventoryNumber}], оценка: {transport.BeginnerScore}");
    }

    private static void PrintTransportResult(bool accepted) =>
        Console.WriteLine(accepted ? "Транспорт принят на баланс." :
            "Транспорт отклонен техосмотром или номер уже занят.");

    private static void PrintThingResult(bool accepted) =>
        Console.WriteLine(accepted ? "Вещь добавлена на баланс." :
            "Номер слишком короткий или уже занят.");

    private static string ReadText(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? throw new EndOfStreamException();
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            var input = ReadText(prompt);
            if (int.TryParse(input, out var value))
                return value;

            Console.WriteLine("Введите целое число.");
        }
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            var input = ReadText(prompt).Replace(',', '.');
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                return value;

            Console.WriteLine("Введите число.");
        }
    }
}
