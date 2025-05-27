using System;

namespace MyApp.Models
{

    public class Car
    {

        private string make;
        private string model;


        public int Year { get; set; }


        internal void SetMakeAndModel(string make, string model)
        {
            this.make = make;
            this.model = model;
        }


        protected virtual void DisplayInfo()
        {
            Console.WriteLine($"Make: {make}");
            Console.WriteLine($"Model: {model}");
            Console.WriteLine($"Year: {Year}");
        }
    }

    public class ElectricCar : Car
    {

        private double batteryCapacity;


        public void SetBatteryCapacity(double capacity)
        {
            this.batteryCapacity = capacity;
        }


        protected override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Battery Capacity: {batteryCapacity} kWh");
        }
    }
}

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MyApp.Models.Car myCar = new MyApp.Models.Car();



            myCar.SetMakeAndModel("Toyota", "Camry");
            myCar.Year = 2023;

            MyApp.Models.ElectricCar myElectricCar = new MyApp.Models.ElectricCar();
            myElectricCar.SetMakeAndModel("Tesla", "Model 3");
            myElectricCar.Year = 2023;
            myElectricCar.SetBatteryCapacity(75.0);


        }
    }
}