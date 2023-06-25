using NUnit.Framework;
using UnityEngine;

public class PlayerControllerTests
{
    private MockPlayerController playerController;

    [SetUp]
    public void Setup()
    {
        GameObject playerObject = new GameObject();
        playerController = playerObject.AddComponent<MockPlayerController>();
    }

    [Test]
    public void TestMockPlayerMove()
    {
        GameObject testObject = new GameObject();
        testObject.AddComponent<BoxCollider2D>();
        MockPlayerController mockPlayerController = testObject.AddComponent<MockPlayerController>();

        mockPlayerController.Move(Vector2.right, () => { });

        Assert.AreEqual(new Vector2(1, 0), (Vector2)testObject.transform.position);

        mockPlayerController.Move(Vector2.up, () => { });

        Assert.AreEqual(new Vector2(1, 1), (Vector2)testObject.transform.position);

        mockPlayerController.Move(Vector2.left, () => { });

        Assert.AreEqual(new Vector2(0, 1), (Vector2)testObject.transform.position);

        mockPlayerController.Move(Vector2.down, () => { });

        Assert.AreEqual(new Vector2(0, 0), (Vector2)testObject.transform.position);
    }
}
