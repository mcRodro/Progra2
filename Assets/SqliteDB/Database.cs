using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Database /*: MonoBehaviour*/
{
    private string connectionPath;
    private IDbConnection dbconn;

    public Database()
    {
        connectionPath = "URI=file:" + Application.dataPath + "/Ranking.s3db"; //Path to database.

        //DropTableRankingRecords();
        CrateTableRankingRecords();
    }

    private void CreateConnection()
    {
        dbconn = new SqliteConnection(connectionPath);
    }

    private void DropTableRankingRecords()
    {
        try
        {
            CreateConnection();
            dbconn.Open();

            IDbCommand command = dbconn.CreateCommand();
            string query =
                "DROP TABLE IF EXISTS RankingRecords";
            command.CommandText = query;
            command.ExecuteReader();

            command.Dispose();
            command = null;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            dbconn.Close();
            dbconn = null;
        }
    }

    private void CrateTableRankingRecords()
    {
        try
        {
            CreateConnection();
            dbconn.Open();

            IDbCommand command = dbconn.CreateCommand();
            string query = 
                "CREATE TABLE IF NOT EXISTS RankingRecords ( " +
                    "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Name VARCHAR(200) NOT NULL, " +
                    "Stage INTEGER NOT NULL, " +
                    "Score INTEGER NOT NULL)";
            command.CommandText = query;
            command.ExecuteReader();

            command.Dispose();
            command = null;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            dbconn.Close();
            dbconn = null;
        }
    }

    public void AddRankingRecord(RankingModel record/*string name, int stage, int score*/)
    {
        try
        {
            CreateConnection();
            dbconn.Open();

            IDbCommand dbcmd = dbconn.CreateCommand();

            string query = string.Format(
                "INSERT INTO RankingRecords " +
                    "(Name, Stage, Score) " +
                    "VALUES ('{0}',{1},{2})", record.NameValue, record.StageValue, record.ScoreValue);

            dbcmd.CommandText = query;
            dbcmd.ExecuteReader();

            dbcmd.Dispose();
            dbcmd = null;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            dbconn.Close();
            dbconn = null;
        }
    }

    public List<RankingModel> GetAllRankingRecords()
    {
        var records = new List<RankingModel>();

        try
        {
            CreateConnection();
            dbconn.Open();

            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT Id, Name, Stage, Score FROM RankingRecords";
            dbcmd.CommandText = sqlQuery;

            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int stage = reader.GetInt32(2);
                int score = reader.GetInt32(3);

                var record = new RankingModel(name, stage, score);
                record.Id = id;
                records.Add(record);

                Debug.Log(string.Format("id: {0} \t name: {1} \t stage: {2} \t score: {3}", id, name, stage, score));
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            dbconn.Close();
            dbconn = null;
        }

        return records;
    }
}
