﻿using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using StarWars.Lib;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyMacroMoveRotateTests
    {
        public RegisterIoCDependencyMacroMoveRotateTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        }

        [Fact]
        public void Execute_ShouldRegisterMacroMoveAndRotate()
        {
            var moveSpec = new[] { "MoveCommand1", "MoveCommand2" };
            var rotateSpec = new[] { "RotateCommand1", "RotateCommand2" };

            var moveCommandMocks = moveSpec.Select(cmd =>
            {
                var mock = new Mock<Hwdtech.ICommand>();
                IoC.Resolve<Hwdtech.ICommand>("IoC.Register", cmd, (object[] args) => mock.Object).Execute();
                return mock;
            }).ToArray();

            var rotateCommandMocks = rotateSpec.Select(cmd =>
            {
                var mock = new Mock<Hwdtech.ICommand>();
                IoC.Resolve<Hwdtech.ICommand>("IoC.Register", cmd, (object[] args) => mock.Object).Execute();
                return mock;
            }).ToArray();

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Specs.Move", (object[] args) => moveSpec).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Specs.Rotate", (object[] args) => rotateSpec).Execute();

            new RegisterIoCDependencyMacroMoveRotate().Execute();

            var moveMacro = IoC.Resolve<Hwdtech.ICommand>("Macro.Move");
            moveMacro.Execute();
            foreach (var mock in moveCommandMocks)
            {
                mock.Verify(cmd => cmd.Execute(), Times.Once());
            }

            var rotateMacro = IoC.Resolve<Hwdtech.ICommand>("Macro.Rotate");
            rotateMacro.Execute();
            foreach (var mock in rotateCommandMocks)
            {
                mock.Verify(cmd => cmd.Execute(), Times.Once());
            }
        }
    }
}
