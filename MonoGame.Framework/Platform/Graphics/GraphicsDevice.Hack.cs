using MonoGame.OpenGL;

namespace Microsoft.Xna.Framework.Graphics;

public partial class GraphicsDevice
{
    // Note VT:
    // J'ai écrie ce code pour re-utiliser le shader de monogame.
    // Lorsque ImGUI draw; il écrasait le shader.
    // Avent je placer a null le shader et appellait ActivateShaderProgram dans PlatformApplyState

    /// <summary>
    /// Reset the shader program.
    /// Use it only when needed.
    /// </summary>
    public void InvalidShader()
    {
        //_shaderProgram = null;
        if (_shaderProgram != null)
        {
            GL.UseProgram(_shaderProgram.Program);
            GraphicsExtensions.CheckGLError();
        }
    }
}
