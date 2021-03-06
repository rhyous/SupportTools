﻿using Rhyous.ServiceManager.Model;

namespace Rhyous.ServiceManager.Business
{
    public class ExampleData
    {
        public ServiceCollection Services { get; set; }

        public ExampleData()
        {
            Services = new ServiceCollection();
            Service s1 = new Service()
            {
                DisplayName = "SQL Active Directory Helper Service",
                ServiceName = "MSSQLServerADHelper100"
            };
            s1.UpdateService();
            Services.Add(s1);

            Service s2 = new Service()
            {
                DisplayName = "SQL Server (SQLEXPRESS)",
                ServiceName = "MSSQL$SQLEXPRESS"
            };
            s2.UpdateService();
            Services.Add(s2);

            Service s3 = new Service()
            {
                DisplayName = "SQL Server Agent (SQLEXPRESS)",
                ServiceName = "SQLAgent$SQLEXPRESS"
            };
            s3.UpdateService();
            Services.Add(s3);

            Service s4 = new Service()
            {
                DisplayName = "SQL Server Browser",
                ServiceName = "SQLBrowser"
            };
            s4.UpdateService();
            Services.Add(s4);

            Service s5 = new Service()
            {
                DisplayName = "SQL Server VSS Writer",
                ServiceName = "SQLWriter"
            };
            s5.UpdateService();
            Services.Add(s5);
        }
    }
}
