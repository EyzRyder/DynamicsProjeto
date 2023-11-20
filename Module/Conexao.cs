using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsProjeto
{
  public class Conexao // nome da classe defenido pelo usuario
  {

    private CrmServiceClient crmServiceClientTrinamento; // variavel do tip CrmServiceClient

    public CrmServiceClient ObterConexao() //metodo que devolve uma CrmServiceClient
    {
      //variavel contendo a ConnectionString
      var connectionStringCRM = @"AuthType=OAuth;
    Username = userdyn365@treindio.onmicrosoft.com;
    Password = Pass@word99; SkipDiscovery = True;
    AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
    RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
    URL = https://org07de2ee3.crm2.dynamics.com/main.aspx;";

      if (crmServiceClientTrinamento == null) // Verificar se a variavel crmServiceClientTrinamento esta vazia
      {
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // define o protocolo seguranca
        crmServiceClientTrinamento = new CrmServiceClient(connectionStringCRM); // executar a conexao
      }
      return crmServiceClientTrinamento; // retorne a conexao para o metodo chamador
    }

  }
}