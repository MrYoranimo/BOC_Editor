﻿using Koko.RunTimeGui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace BOC_Editor {
	public class Editor : Game {

		private IWindowState _CurrentState;

		public static Vector2 DefaultWindowSize;

		private GraphicsDeviceManager _graphics;
		private BoxingViewportAdapter _ViewportAdapter;
		private OrthographicCamera _Camera;
		private SpriteBatch _spriteBatch;

		private readonly GUI Gui = GUI.Gui;

		public Editor() {
			_CurrentState = EditorWindowDict.GetState(EditorWindowDict.EWindowType.Overview);

			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize() {
			_graphics.IsFullScreen = false;
			_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
			_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
			_graphics.ApplyChanges();

			FontHelper.Add(Content.RootDirectory);

			var panel = new Panel();
			var label = new Label();
			var label2 = new Label();

			panel.ChildComponents.Add(label);
			panel.ChildComponents.Add(label2);
			Gui.ChildComponents.Add(panel);

			var panel2 = new Panel();
			var button = new Button();
			var label3 = new Label();

			panel2.ChildComponents.Add(button);
			panel2.ChildComponents.Add(label3);
			Gui.ChildComponents.Add(panel2);

			Gui.Init();

			base.Initialize();
		}

		protected override void LoadContent() {
			_ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1600, 1024);
			_Camera = new OrthographicCamera(_ViewportAdapter);

			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime) {
			Gui.Update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.DarkSlateGray);

			var view = _Camera.GetViewMatrix();

			_spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend, transformMatrix: view);

			_spriteBatch.End();

			_spriteBatch.Begin(samplerState: SamplerState.LinearClamp, transformMatrix: view);

			Gui.Draw(_spriteBatch);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
