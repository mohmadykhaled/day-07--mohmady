using System;

namespace day_07
{


    #region problem 1

    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }

        public Car() { }

        public Car(int id)
        {
            Id = id;
        }

        public Car(int id, string brand)
        {
            Id = id;
            Brand = brand;
        }

        public Car(int id, string brand, decimal price)
        {
            Id = id;
            Brand = brand;
            Price = price;
        }
    }

    public class Program
    {
        public static void Main()
        {
            Car car1 = new Car();
            Car car2 = new Car(1);
            Car car3 = new Car(2, "Toyta");
            Car car4 = new Car(3, "BMW", 35000m);

            Console.WriteLine($"Car1: Id={car1.Id}, Brand={car1.Brand}, Price={car1.Price}");
            Console.WriteLine($"Car2: Id={car2.Id}, Brand={car2.Brand}, Price={car2.Price}");
            Console.WriteLine($"Car3: Id={car3.Id}, Brand={car3.Brand}, Price={car3.Price}");
            Console.WriteLine($"Car4: Id={car4.Id}, Brand={car4.Brand}, Price={car4.Price}");
        }
    }
    /*the compiler assumes that if you're explicitly defining constructors
     you want to take full control of the instantiation process
     including determining which constructors should exist and their behaviorDefault 
    Constructor Generation automatically generates .*/
    #endregion

    #region problem 2

    public class Calculator
    {
        public int Sum(int x, int y)
        {
            return x + y;
        }

        public int Sum(int x, int y, int z)
        {
            return x + y + z;
        }

        public double Sum(double x, double y)
        {
            return x + y;
        }
    }

    public class Program
    {
        public static void Main()
        {
            Calculator calculator = new Calculator();

            int result1 = calculator.Sum(5, 10);
            Console.WriteLine($"Sum of two integers: {result1}");

            int result2 = calculator.Sum(5, 10, 15);
            Console.WriteLine($"Sum of three integers: {result2}");

            double result3 = calculator.Sum(5.5, 10.5);
            Console.WriteLine($"Sum of two doubles {result3}");
        }
    }
    /*use of the same method name for different parameter types or counts
      making the code more intuitive and consistent 
    can different input types or scenarios without
    duplicating logic or creating separate methods
    with redundant functionality.*/

    #endregion

    #region problem 3
    public class Parent
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Parent(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Child : Parent
    {
        public int Z { get; set; }
        public Child(int x, int y, int z) : base(x, y)
        {
            Z = z;
        }
    }

    public class Program
    {
        public static void Main()
        {
            Child child = new Child(10, 20, 30);

            Console.WriteLine($"X={child.X}, Y={child.Y}, Z={child.Z}");
        }
    }
    /*  reuse the initialization logic from the base class
      You don't need to duplicate code to initialize properties
    that are already defined the base class and helpThis helps maintain a clear and consistent initialization process*/

    #endregion

    #region problem 4
    namespace example
    {
        public class Parent
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Parent(int x, int y)
            {
                X = x;
                Y = y;
            }

            public virtual int Product()
            {
                return X * Y;
            }
        }

        public class Child : Parent
        {
            public int Z { get; set; }

            public Child(int x, int y, int z) : base(x, y)
            {
                Z = z;
            }

            public override int Product()
            {
                return X * Y * Z;
            }
        }
    }


    namespace example
    {
        public class Program
        {
            public static void Main()
            {
                Child child = new Child(2, 4 ,6);
                Parent parentRef = child;
                Console.WriteLine($"Parent reference: Product() = {parentRef.Product()}");
                Console.WriteLine($"Child reference: Product() = {child.Product()}");
            }
        }
    }
    /*new keyword: Hides the base class method. If you call
the method using a base class reference the base class method is called
   override keyword:if the reference is of the parent type as long as the object is of the child type */
    #endregion

    #region problem 5
    namespace example
    {
        public class Parent
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Parent(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }
        }

        public class Child : Parent
        {
            public int Z { get; set; }

            public Child(int x, int y, int z) : base(x, y)
            {
                Z = z;
            }

            public override string ToString()
            {
                return $"({X}, {Y}, {Z})";
            }
        }
    }

    namespace ex
    {
        public class Program
        {
            public static void Main()
            {
                Parent parent = new Parent(5, 10);
                Child child = new Child(5, 10, 15);
                Console.WriteLine(parent.ToString()); 
                Console.WriteLine(child.ToString());  
            }
        }
    }
    /*  meaningful string representation of the object making it easier to inspect and debug
   Logging and Display Custom ToString() implementations allow formats which are useful in applications*/
    #endregion

    #region problem 6

    namespace Shapes
    {
        public interface IShape
        {
            double Area { get; }
            void Draw();
        }

        public class Rectangle : IShape
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public double Area => Width * Height;

            public void Draw()
            {
                Console.WriteLine($"Drawing a rectangle with width {Width} and height {Height}");
            }
        }
    }


    namespace Shapes
    {
        public class Program
        {
            public static void Main()
            {
                IShape rectangle = new Rectangle(5, 10);
                rectangle.Draw();
                Console.WriteLine($"Area of rectangle: {rectangle.Area}");
             
            }
        }
    }
    /*You can't create an instance of  interface directly becauseNo Implementation
    nterface only defines method signatures
    To create an object, you need a class that implements the interface
    */
    #endregion

    #region problem 7
    namespace Shapes
    {
        public interface IShape
        {
            double Area { get; }
            void Draw();
            void PrintDetails()
            {
                Console.WriteLine($"Area: {Area}");
            }
        }

        public class Circle : IShape
        {
            public double Radius { get; set; }

            public Circle(double radius)
            {
                Radius = radius;
            }

            public double Area => Math.PI * Radius * Radius;

            public void Draw()
            {
                Console.WriteLine($"Drawing a circle with radius {Radius}");
            }
        }
    }


    namespace Shapes
    {
        public class Program
        {
            public static void Main()
            {
                IShape circle = new Circle(10);
                circle.PrintDetails();
                circle.Draw();
            }
        }
    }
    /* If new methods are added with default behavior
      classes that implement the interface don't need to be modified
      which is especially useful for maintaining libraries */
    #endregion

    #region problem 8
    namespace Vehicles

    {
        public interface IMovable
        {
            void Move();
        }

        public class Car : IMovable
        {
            public void Move()
            {
                Console.WriteLine("The car is moving.");
            }
        }
    }

    namespace Vehicles

    {
        public class Program
        {
            public static void Main()
            {
                IMovable movableCar = new Car();
                movableCar.Move();
            }
        }
    }

             /* allows you to treat objects of different classes that implement and This approach promotes the use of polymorphism allowing you to
           switch out implementations without changing the code that uses the interface*/
    #endregion

}

