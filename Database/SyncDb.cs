using System.Linq;
using Database.Db;
using Database.Models.DoList;

namespace Database;

//实现本地数据库同步功能
public static class SyncDb
{
    //仅同步本地数据库things表
    public static void SyncThings()
    {
        using (var localDb = new ContextLocal())
        {
            using (var remoteDb = new ContextRemote())
            {
                if (remoteDb.Database.CanConnect())
                {
                    remoteDb.Database.EnsureCreated();
                    var localThings = localDb.Things.ToList();
                    var remoteThings = remoteDb.Things.ToList();
                    //将本地库数据同步到远程库
                    foreach (var localThing in localThings)
                    {
                        var remoteThing =
                            remoteThings.FirstOrDefault(t => t.CreateTimeStamp == localThing.CreateTimeStamp);
                        if (remoteThing == null)
                        {
                            remoteDb.Things.Add(new Thing()
                            {
                                Content = localThing.Content,
                                Done = localThing.Done,
                                CreateTime = localThing.CreateTime,
                                FinishedTime = localThing.FinishedTime,
                                Remind = localThing.Remind,
                                RemindTime = localThing.RemindTime,
                                CreateTimeStamp = localThing.CreateTimeStamp,
                                UpdateTimeStamp = localThing.UpdateTimeStamp
                            });
                        }
                        else
                        {
                            if (localThing.UpdateTimeStamp > remoteThing.UpdateTimeStamp)
                            {
                                remoteThing.Content = localThing.Content;
                                remoteThing.Done = localThing.Done;
                                remoteThing.CreateTime = localThing.CreateTime;
                                remoteThing.FinishedTime = localThing.FinishedTime;
                                remoteThing.Remind = localThing.Remind;
                                remoteThing.RemindTime = localThing.RemindTime;
                                remoteThing.CreateTimeStamp = localThing.CreateTimeStamp;
                                remoteThing.UpdateTimeStamp = localThing.UpdateTimeStamp;
                            }
                        }
                    }

                    //将远程库数据同步到本地库
                    foreach (var remoteThing in remoteThings)
                    {
                        var localThing =
                            localThings.FirstOrDefault(t => t.CreateTimeStamp == remoteThing.CreateTimeStamp);
                        if (localThing == null)
                        {
                            localDb.Things.Add(new Thing()
                                {
                                    Content = remoteThing.Content,
                                    Done = remoteThing.Done,
                                    CreateTime = remoteThing.CreateTime,
                                    FinishedTime = remoteThing.FinishedTime,
                                    Remind = remoteThing.Remind,
                                    RemindTime = remoteThing.RemindTime,
                                    CreateTimeStamp = remoteThing.CreateTimeStamp,
                                    UpdateTimeStamp = remoteThing.UpdateTimeStamp
                                }
                            );
                        }
                        else
                        {
                            if (remoteThing.UpdateTimeStamp > localThing.UpdateTimeStamp)
                            {
                                localThing.Content = remoteThing.Content;
                                localThing.Done = remoteThing.Done;
                                localThing.CreateTime = remoteThing.CreateTime;
                                localThing.FinishedTime = remoteThing.FinishedTime;
                                localThing.Remind = remoteThing.Remind;
                                localThing.RemindTime = remoteThing.RemindTime;
                                localThing.CreateTimeStamp = remoteThing.CreateTimeStamp;
                                localThing.UpdateTimeStamp = remoteThing.UpdateTimeStamp;
                            }
                        }
                    }

                    localDb.SaveChanges();
                    remoteDb.SaveChanges();
                }
            }
        }

        {
        }
    }
}