
public class ZombieController : ZombieContext
{
    // Start is called before the first frame update
    void Start()
    {
        InitializeContext();
    }

    private void FixedUpdate()
    {
        ManageState(this);
    }        
}