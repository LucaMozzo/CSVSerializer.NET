Module MainModule

    Sub Main()
        Console.WriteLine("Simple serialization")
        SimpleSerialization.Serialize().Wait()

        Console.WriteLine("Object serialization")
        SimpleSerialization.Serialize().Wait()
    End Sub
End Module
