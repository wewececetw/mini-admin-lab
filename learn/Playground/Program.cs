int a = 10;
int b = a;
b = 99;
Console.WriteLine($"a = {a}, b = {b}");

int[] x = [10, 20];
int[] y = x;
y[0] = 99;
Console.WriteLine($"x[0] = {x[0]}, y[0] = {y[0]}");
