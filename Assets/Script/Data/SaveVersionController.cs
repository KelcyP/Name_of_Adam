using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ���� ���̺� �������� ���Ἲ�� üũ�ϴ� Ŭ����

public class SaveVersionController
{
    // �� Unity Version�� ���� History�� �ݵ�� ����ؾ� �մϴ�!
    // ���� ���� ��Ī�� �� �� �ݵ�� ������ üũ�ϰ�, ������Ʈ�� �ʿ��� ��쿡�� ���̱׷��̼��� �����ؾ� �մϴ�.

    private List<string> versionHistory = new List<string>
    {
        "1.0.0-release",
        "1.0.1-release",
    };

    public bool IsValildVersion()
        => GameManager.OutGameData.GetVersion().Equals(Application.version);

    public bool CheckNeedMigration()
    {
        if (IsValildVersion() == true)
        {
            Debug.Log("The OutGameData is up to data.");
            return false;
        }

        Debug.Log($"Data Version is not matched! Save Version : {GameManager.OutGameData.GetVersion()} / Build Version {Application.version}");
        return true;
    }

    public void MigrateData()
    {
        string userVersion = GameManager.OutGameData.GetVersion();
        int userVersionIndex = versionHistory.IndexOf(userVersion);

        if (userVersionIndex == -1)
        {
            Debug.LogError("Invalid Version! Check [ProjectSetting] and [SaveVersionController]!");
            return;
        }

        // ������ ������(Linearly)���� ������Ʈ �Ǿ�� �մϴ�.
        while (userVersion.Equals(Application.version) == false)
        {
            switch (userVersion)
            {
                // ���� ������Ʈ���� ���� �ʿ� �� �߰�
                case "1.0.0-release":
                    GameManager.SaveManager.DeleteSaveData();

                    // ���� ���� ����Ű(PrivateKey) �߰�
                    foreach (HallUnit unit in GameManager.OutGameData.FindHallUnitList())
                    {
                        if (string.IsNullOrEmpty(unit.PrivateKey))
                            unit.PrivateKey = GameManager.CreatePrivateKey();
                    }
                    break;
            }

            userVersionIndex++;
            GameManager.OutGameData.SetVersion(versionHistory[userVersionIndex]);
            userVersion = GameManager.OutGameData.GetVersion();
        }

        GameManager.OutGameData.SaveData();
    }
}
