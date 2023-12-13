var outer = Task.Factory.StartNew(() => //вложенные Task
{
    Console.WriteLine("Outer task starting...");
    var inner = Task.Factory.StartNew(() =>
    {
        Console.WriteLine("Inner task starting...");
        Thread.Sleep(2000);
        Console.WriteLine("Inner task finished...");
    },TaskCreationOptions.AttachedToParent);
});
outer.Wait();
Console.WriteLine("End of Main");

Task[] tasks = new Task[3]; //Массив Task
for(var i=0; i<tasks.Length; i++)
{
    tasks[i] = new Task(() =>
    {
        Thread.Sleep(1000);
        Console.WriteLine($"Task{i} finished");
    });
    tasks[i].Start();
}
Console.WriteLine("Завершение метода Main");
Task.WaitAll(tasks);
//Возвращение результатов из задач
int n1 = 4, n2 = 5;
Task<int> sumTask= new Task<int>(()=>Sum(n1,n2));
sumTask.Start();

int result = sumTask.Result;
Console.WriteLine($"{n1} + {n2} = {result}");


int Sum(int a,int b)=>a+b;

Task<Human> defaultHumanTask = new Task<Human>(() => new Human("Ted", 35));
defaultHumanTask.Start();

Human defaultHuman=defaultHumanTask.Result;
Console.WriteLine($"{defaultHuman.Name} - {defaultHuman.Age}");

//Задачи продолжения
Task task1 = new Task(() =>
{
    Console.WriteLine($"Id задачи: {Task.CurrentId}");
});
Task task2 = task1.ContinueWith(PrintTask);
task1.Start();
task2.Wait();
Console.WriteLine("End");
void PrintTask(Task t)
{
    Console.WriteLine($"Id задачи: {Task.CurrentId}");
    Console.WriteLine($"Id предыдущей задачи: {t.Id}");
    Thread.Sleep(3000);
}
Task<int> sumTask1 = new Task<int>(()=>Sum(4,9));
Task printTask=sumTask1.ContinueWith(task=>PrintResult(task.Result));
sumTask1.Start();

printTask.Wait();
Console.WriteLine("End");

void PrintResult(int sum) => Console.WriteLine($"Sum: {sum}");
record class Human(string Name, int Age);
