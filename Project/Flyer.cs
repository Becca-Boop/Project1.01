using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Flyer : Enemy
    {
        private bool flag = true;   // while testing, once pathfind once
        private int state = 0;

        static Vector2[] Moves = new Vector2[]
        // can move in any of eight directions from the current node (grid square)
        {
            new Vector2(0, 1),
            //new Vector2(1, 1),
            new Vector2(1, 0),
            //new Vector2(1, -1),
            new Vector2(0, -1),
            //new Vector2(-1, -1),
            new Vector2(-1, 0),
            //new Vector2(-1, 1),
        };

        public Flyer(Game game, Texture2D _texture) : base(game, _texture)
        {
            ignoreBlocks = false;
        }

        public override Vector2 GetMove(float dt)
        {
            //Console.WriteLine("Path finding!");


            Vector2 startingNode = this.GetNode();
            Vector2 targetNode = Game.Player.GetNode();
            if (startingNode == targetNode)
            {
                Console.WriteLine("Already with the player!");
                this.state = 3;
                //playerFound = new PathStep(null, startingNode);

                Vector2 moveDir = Position - Game.Player.Position;
                moveDir.Normalize();
                float distance = Vector2.Distance(Position, Game.Player.Position);

                Vector2 moveVector = new Vector2(-moveDir.X * dt / 10, -moveDir.Y * dt / 10);
                Game.message("At the player");
                return moveVector;
            }


            if (this.state == 0)
            {
                // Move to centre of grid so we can path find

                if (this.Centred())
                {
                    this.state = 1;
                }
                else
                {
                    Vector2 moveDir = Position - this.GetNodeCoords();
                    moveDir.Normalize();
                    float distance = Vector2.Distance(Position, Game.Player.Position);

                    Vector2 moveVector = new Vector2(-moveDir.X * dt / 10, -moveDir.Y * dt / 10);
                    Game.message("Aligning");
                    return moveVector;
                }
            }

            HashSet<Vector2> visitedNodes = new HashSet<Vector2>();
            List<PathStep> steps = new List<PathStep>();
            PathStep playerFound = null;
            visitedNodes.Add(startingNode);
            steps.Add(new PathStep(null, startingNode));
            int pathStepPosition = 0;


            while (pathStepPosition < steps.Count && playerFound == null)
            {
                //Console.WriteLine(pathStepPosition);
                PathStep currentStep = steps[pathStepPosition];
                //Console.WriteLine(currentStep.position);
                pathStepPosition++;

                // move in each of eight directions
                for (int i = 0; i < 4; i++)
                {
                    Vector2 newNode = new Vector2(currentStep.position.X + Moves[i].X, currentStep.position.Y + Moves[i].Y);
                    if (newNode.X == targetNode.X && newNode.Y == targetNode.Y)
                    {
                        //Console.WriteLine("Found the player!");
                        playerFound = new PathStep(currentStep, newNode);
                        continue;
                    }
                    if (visitedNodes.Contains(newNode))
                    {
                        //Console.WriteLine("Been here before!");
                        continue;   // already been here, no need to do again
                    }
                    if (Game.blockedNodes.Contains(newNode))
                    {
                        //Console.WriteLine("Blocked!");
                        continue;   // route blocked this way so abandon
                    }
                    steps.Add(new PathStep(currentStep, newNode));
                    visitedNodes.Add(newNode);

                }

            }

            if (playerFound == null)
            {
                Console.WriteLine("Pathing failed!");
                return new Vector2(0, 0);
            }




            Vector2 firstStep = new Vector2(0, 0);
            PathStep checker = playerFound;
            int count = 0;
            while (checker != null)
            {
                //Console.WriteLine(checker.position);
                if (checker.previous != null)
                {
                    firstStep = checker.position;
                }
                checker = checker.previous;
                count++;
            }

            if (flag)
            {
                flag = false;
                Console.WriteLine(position);
                Console.WriteLine(Game.Player.position);
                Console.WriteLine(startingNode);
                Console.WriteLine(targetNode);
            }
            //Console.WriteLine(count);

            if (firstStep == new Vector2(0, 0))
            {
                return new Vector2(0, 0);
            }
            else
            {
                Vector2 moveDir = startingNode - firstStep;
                Game.message(startingNode.ToString() + "->" + firstStep.ToString() + " ... " + this.Position.ToString());

                moveDir.Normalize();
                float distance = Vector2.Distance(Position, Game.Player.Position);

                Vector2 moveVector = new Vector2(-moveDir.X * dt / 10, -moveDir.Y * dt / 10);
                return moveVector;

            }
        }


        public override void Bump()
        {
            Console.WriteLine("F-Bump");
            this.state = 0;
        }

    }
}
