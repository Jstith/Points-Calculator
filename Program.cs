internal class Program
{

    private double target_amex_point_value = 0.01;
    private double exchange_rate_amex_delta = 1.0;

    /*
     * Baseline values to compare against are:
     * 1. Cost of flight calculated by purchasing on airline's website (points earned from purchase are subtracted from points valued)
     * 2. Cost of flight by paying credit card cost off directly with Amex Points (flat rate of 0.06 cents per point or 1000 points = 60 dollars)
     */

    private double[] DeltaOptions()
    {
        Console.WriteLine("\nEnter total cost of trip on Delta Website in dollars:");
        Console.Write("> ");
        string raw_input = Console.ReadLine();
        double dollar_cost_delta = double.Parse(raw_input);
        int earned_points = CalculateAmexPoints(dollar_cost_delta, false);

        Console.WriteLine("\nEnter total cost of trip on Delta Website in miles:");
        Console.Write("> ");
        raw_input = Console.ReadLine();
        double miles_cost_delta = double.Parse(raw_input);

        Console.WriteLine("\nEnter total cost of miles conversion (add for all flights):");
        Console.Write("> ");
        raw_input = Console.ReadLine();
        double conversion_cost_delta = double.Parse(raw_input);

        // Since AMEX points earned from the purchase can be exchanged 1 to 1 with Delta Miles, count all points earned from purchase with card as miles
        double miles_value = CalculateMileValue(miles_cost_delta, dollar_cost_delta, conversion_cost_delta);
        // CompareValue(miles_value);

        return new double[] { dollar_cost_delta, earned_points, miles_cost_delta, conversion_cost_delta, miles_value};
    }

    private double[] AmexOptions()
    {
        // Get price of trip in dollars from amex
        Console.WriteLine("\nEnter total cost of trip on Amex Travel in dollars:");
        Console.Write("> ");
        string raw_input = Console.ReadLine();
        double dollar_cost_amex = double.Parse(raw_input);
        int earned_points = CalculateAmexPoints(dollar_cost_amex, true);

        // Get price of trip in points from amex
        Console.WriteLine("\nEnter total cost of trip on Amex Travel in points:");
        Console.Write("> ");
        raw_input = Console.ReadLine();
        double points_cost_amex = double.Parse(raw_input);

        // Get points value based on cost in points and miles, and no transfer cost for amex
        double points_value = CalculateMileValue(points_cost_amex, dollar_cost_amex, 0);
        // CompareValue(points_value);

        return new double[] { dollar_cost_amex, earned_points, points_cost_amex, 0, points_value};
    }


    private int CalculateAmexPoints(double dollar_cost, bool amex_travel)
    {
        if (amex_travel)
        {
            return (int)(dollar_cost * 5.0); 
        }
        return (int)dollar_cost;
    }

    private double CalculateMileValue(double miles, double cost, double mile_conversion_cost)
    {
        return (cost - mile_conversion_cost) / miles;
    }

    private void CompareValue(double point_value)
    {
        if(point_value >= target_amex_point_value)
        {
            Console.WriteLine("Point/Mile value of " + point_value + " is BETTER than target value of " + target_amex_point_value);
        }
        else
        {
            Console.WriteLine("Point/Mile value of " + point_value + " is WORSE than target value of " + target_amex_point_value);
        }
    }

    // return new double[] { dollar_cost_delta, earned_points, miles_cost_delta, conversion_cost_delta, miles_value};

    private void DisplayComparisons(double[] delta_data, double[] amex_data)
    {
        double delta_adjusted_dollars_target = delta_data[0] - delta_data[1] * target_amex_point_value;
        double amex_adjusted_dollars_target = amex_data[0] - amex_data[1] * target_amex_point_value;
        double delta_point_cost_dollar_target = delta_data[2] * target_amex_point_value;
        double amex_point_cost_dollar_target = amex_data[2] * target_amex_point_value;

        Console.WriteLine("\n\n");
        Console.WriteLine("------------ Comparing Base Costs in dollars and points ------------");
        Console.WriteLine("Booking with EXTERNAL in dollars: $" + string.Format("{0:0.00}", delta_data[0]) + " and " + delta_data[1] + " points earned.");
        Console.WriteLine("Booking with AMEX     in dollars: $" + string.Format("{0:0.00}", amex_data[0]) + " and " + amex_data[1] + " points earned.");
        Console.WriteLine("Booking with EXTERNAL in points:  " + delta_data[2] + " and $" + delta_data[3] + " conversion cost.");
        Console.WriteLine("Booking with AMEX     in points:  " + amex_data[2] + " and $" + amex_data[3] + " conversion cost.");

        Console.WriteLine("\n\n");
        Console.WriteLine("------------ Point Valuations (If I buy with points, what value am I getting for them?) ------------");
        Console.WriteLine("Value of EXTERNAL points are: $" + string.Format("{0:0.0000}", delta_data[4]));
        Console.WriteLine("Value of AMEX     points are: $" + string.Format("{0:0.0000}", amex_data[4]));

        Console.WriteLine("\n\n");
        Console.WriteLine("------------ Comparison of Adjusted Dollar Values (If my points are worth the target value, what's the best deal?) ------------");
        Console.WriteLine("Booking with EXTERNAL in points:  $" + delta_point_cost_dollar_target);
        Console.WriteLine("Booking with AMEX     in points:  $" + amex_point_cost_dollar_target);
        Console.WriteLine("Booking with EXTERNAL in dollars: $" + delta_adjusted_dollars_target);
        Console.WriteLine("Booking with AMEX     in dollars: $" + amex_adjusted_dollars_target);

        Console.WriteLine("\n");
        Console.WriteLine("(Note: AmexTravel.com points are 1 cent, Amex Platinum credit charge points are 0.6 cents)");

    }

    private static void Main(string[] args)
    {
        Program p = new Program();
        Console.WriteLine("Enter target valuation of Amex Platinum Points in dollars: (Enter for default value " + p.target_amex_point_value + "):");
        Console.Write("> ");
        string? input = Console.ReadLine();
        if (input != null && input.Length != 0)
        {
            p.target_amex_point_value = Double.Parse(input) / 100;
        }

        Console.WriteLine("\nTarget point value set to: " + p.target_amex_point_value);
        
        double[] delta_data = p.DeltaOptions();
        double[] amex_data = p.AmexOptions();

        p.DisplayComparisons(delta_data, amex_data);
    }
}

