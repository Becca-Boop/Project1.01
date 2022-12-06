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

        static Vector2[] Moves = new Vector2[]
        // can move in any of eight directions from the current node (grid square)
        {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(1, -1),
            new Vector2(0, -1),
            new Vector2(-1, -1),
            new Vector2(-1, 0),
            new Vector2(-1, 1),
        };

        public Flyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
            ignoreBlocks = false;
        }

        public override Vector2 GetMove(float dt)
        {
            Vector2 moveDir = Position - Game.Player.Position;
            //Console.WriteLine("Path finding!");


            HashSet<Vector2> visitedNodes = new HashSet<Vector2>();
            List<PathStep> steps = new List<PathStep>();
            Vector2 startingNode = this.GetNode();
            Vector2 targetNode = Game.Player.GetNode();
            PathStep playerFound = null;
            if (startingNode == targetNode)
            {
                Console.WriteLine("Already with the player!");
                playerFound = new PathStep(null, startingNode);
            }
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
                for (int i = 0; i < 8; i++)
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
                    if (!ignoreBlocks && Game.blockedNodes.Contains(newNode))
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
            Console.WriteLine(count);

            if (firstStep == new Vector2(0, 0)) return new Vector2(0, 0);
            moveDir = startingNode - firstStep;

            moveDir.Normalize();
            float distance = Vector2.Distance(Position, Game.Player.Position);

            Vector2 moveVector = new Vector2(-moveDir.X * dt / 10, -moveDir.Y * dt / 10);
            return moveVector;
        }


        public virtual void Bump()
        {
            Console.WriteLine("F-Bump");
        }

    }
}
