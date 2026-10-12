using Dr.Core.Core;

namespace Dr.Core.Systems;

public interface ISystem
{
    void Update(World world, float deltaTime);
}