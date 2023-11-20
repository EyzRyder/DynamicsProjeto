using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicsProjeto
{
    public class DataModel
    {
        public void FetchXML(CrmServiceClient crmService) // recebe como parametro
        {
            string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                <entity name='account'>
                                    <attribute name='name'/>
                                    <attribute name='telephone1'/>
                                    <attribute name='accountid'/>
                                    <attribute name='emailaddress1'/>
                                    <order attribute='name' descending='false' />
                                </entity>
                              </fetch>"; //declara variavel do tipo String contendo o FechZML da consulta
            EntityCollection colecao = crmService.RetrieveMultiple(new FetchExpression(query)); // declara variavel do tipo EntityCollection e executa o metodo RetrieveMultiplo enviando como parametro a variavel query

            foreach (var item in colecao.Entities) // para cada entidade (registro) armazenara na variavel item
            {
                Console.WriteLine(item["name"]); //exibe o atributo name
                if (item.Attributes.Contains("telephone1")) // verificar se retornou o atributo telephone1
                {
                    Console.WriteLine(item["telephone1"]); //exibe o atributo telephone
                }
            }
            Console.ReadKey(); //aguarda o usuario pressionar qualquer tecla para continuar
        }

        public void Create(CrmServiceClient crmService) // recebe como parametro a conexao
        {
            Guid newRecord = new Guid(); // declara variavel do tipo Guid
            Entity newEntity = new Entity("account"); // declara variacel do Entity a partir da entidade Account

            newEntity.Attributes.Add("name", "Conta Criada em Treinamento -" + DateTime.Now.ToString()); // nome
            newEntity.Attributes.Add("telephone1", "11985326471"); // telefone
            newEntity.Attributes.Add("emailaddress1", "contato@provedor.com"); // e-mail

            newRecord = crmService.Create(newEntity); // executa o metodo Create e armazena na variavel newRecord o Guid retornado do novo registro no Dataverse.

            if (newRecord != Guid.Empty) // verificar se a variavel newRecord contem um Guid
            {
                Console.WriteLine("Id do Registro criado: " + newRecord); // exibe informacao no console do usuario
            }
            else
            {
                Console.WriteLine("newRecord nao criado"); //exibe informacao que nao foi criado o registro
            }
            Console.ReadKey(); //aguarda o usuario pressionar qualquer tecla para continuar

        }

        public void UpdateEntity(CrmServiceClient crmService, Guid guidAccount)
        {
            Entity upEntity = new Entity("account", guidAccount);

            upEntity["name"] = "Luiz Prado Update";
            upEntity["telephone1"] = "11978526941";
            upEntity["emailaddress1"] = "contato@meuprovedor.com";

            crmService.Update(upEntity);
        }

        public void DeleteEntity(CrmServiceClient crmService, Guid guidAccount)
        {
            var entityDelete = crmService.Retrieve("account", guidAccount, new ColumnSet("name"));
            Console.WriteLine("===============================================================");
            Console.WriteLine("Confima a exclusao da conta: " + entityDelete["name"] + " ? (S/N)");
            var confirm = Console.ReadLine();
            if (confirm == "S" || confirm == "s")
            {
                if (entityDelete.Attributes.Count > 0)
                {
                    crmService.Delete("account", guidAccount);
                    Console.WriteLine("Conta Excluída!");
                    Console.WriteLine("===============================================================");
                    Console.ReadKey();
                }
            }
        }
    }
}