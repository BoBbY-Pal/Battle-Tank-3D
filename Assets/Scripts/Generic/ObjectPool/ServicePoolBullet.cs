
public class ServicePoolBullet : ServicePool<BulletController>
{
    private BulletView _bulletView;
    private BulletModel _bulletModel;

    public BulletController GetBullet(BulletModel bulletModel, BulletView bulletView)
    {
        _bulletModel = bulletModel;
        _bulletView = bulletView;
        return GetItem();
    }

    protected override BulletController CreateItem()
    {
        BulletController tankController = new BulletController(_bulletModel, _bulletView);
        return tankController;
    }
}