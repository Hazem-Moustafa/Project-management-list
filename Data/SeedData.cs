﻿using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Data
{
	public class SeedData
	{
		internal static void Populate(ProjectsContext ProjectsContext)
		{
			Project project = ProjectsContext.Project.FirstOrDefault();
			Project project2 = ProjectsContext.Project.FirstOrDefault();

			if (project == null)
			{
				project = new Project { Name = "A Mobile App Development Project" };
				ProjectsContext.Add(project);
				project2 = new Project { Name = "Analysis Project" };
				ProjectsContext.Add(project2);

				for (int i = 1; i <= 5; i++)
				{
					ProjectsContext.Board.Add(
						new Board
						{
							Title = "Board " + i,
							BoardDescription = "Board description for" + i,
							Project = project,
							CreatedDate = DateTime.Now
						}
					);
				}

				for (int i = 1; i <= 10; i++)
				{
					ProjectsContext.Board.Add(
						new Board
						{
							Title = "Board " + i,
							BoardDescription = "Board description for " + i,
							Project = project2,
							CreatedDate = DateTime.Now
						}
					);
				}
				ProjectsContext.SaveChanges();
			}
	
		}
	}
}
