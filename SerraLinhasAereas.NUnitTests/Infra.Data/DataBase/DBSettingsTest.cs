using NUnit.Framework;
using SerraLinhasAereas.Infra.Data.DataBase;
using System.Data.SqlClient;

namespace SerraLinhasAereas.NUnitTests.Infra.Data.DataBase
{
    [TestFixture]
    public class DBSettingsTest
    {
        [Test]
        public void QuandoOMetodoDeConexaoForChamado_EntaoOAtributoDeConnectionStringDeveEstarPreenchidoEAtributoStateDeveTerOValorClosed()
        {
            //Arrange
            SqlConnection conexao;

            //Act
            conexao = DBSettings.Conexao();

            //Assert
            Assert.IsNotEmpty(conexao.ConnectionString);
            Assert.AreEqual(conexao.State.ToString(), "Closed");
        }
    }
}