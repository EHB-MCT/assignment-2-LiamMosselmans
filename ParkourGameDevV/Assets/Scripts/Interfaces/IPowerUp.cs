    using System.Collections;

    public interface IPowerUp
    {
        string PowerUpName { get; set; }
        float Duration { get; set; }

        void Initialize();
        IEnumerator HandlePowerUp();
        void EnablePowerUpEffect();
        void DisablePowerUpEffect();
    }