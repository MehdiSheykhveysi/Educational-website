using Site.Core.DataBase.Context;
using Site.Core.Infrastructures.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class SpReaderRepository : ISpReaderRepository
    {
        private readonly LearningSiteDbContext context;

        public SpReaderRepository(LearningSiteDbContext Context)
        {
            context = Context;
        }

        public async Task<List<SpDTO>> GetSpsAsync(string[,] Parameters, CancellationToken cancellationToken)
        {
            await context.OpenConnectionAsync(cancellationToken);
            DbCommand cmd = context.Command();
            cmd.CommandText = "GetPopularCourse";
            cmd.CommandType = CommandType.StoredProcedure;
            int countParametr = Parameters.GetLength(0);
            for (int i = 0; i < countParametr; i++)
            {
                cmd.Parameters.Add(new SqlParameter { ParameterName = Parameters[i, 0], Value = Convert.ToInt32(Parameters[i, 1]) });
            }

            List<SpDTO> list = new List<SpDTO>();
            using (DbDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken))
            {
                if (reader != null && reader.HasRows)
                {
                    Type entity = typeof(SpDTO);
                    Dictionary<string, PropertyInfo> propDict = new Dictionary<string, PropertyInfo>();
                    PropertyInfo[] props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        SpDTO newobject = new SpDTO();

                        for (int index = 0; index < reader.FieldCount; index++)
                        {
                            if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                            {
                                var info = propDict[reader.GetName(index).ToUpper()];
                                if ((info != null) && info.CanWrite)
                                {
                                    object val = reader.GetValue(index);
                                    info.SetValue(newobject, (val == DBNull.Value) ? null : val, null);
                                }
                            }
                        }
                        list.Add(newobject);
                    }

                }
                return list;

            }
        }
    }
}
