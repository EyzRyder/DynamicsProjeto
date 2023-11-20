using DynamicsProjeto;

internal class Program
{
  public static void Main(string[] args)
  {
    Console.WriteLine("Hello World");

    var crmService = new Conexao().ObterConexao();
    DataModel model = new DataModel();
    // Console.WriteLine(crmService);
    model.FetchXML(crmService);
    // model.Create(crmService);
    // model.UpdateEntity(crmService,new Guid("79225678-9fed-ecll-bb3c-00224838649d"));
    Console.ReadKey();
  }

}
