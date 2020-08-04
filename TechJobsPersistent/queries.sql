--Part 1 //DESCRIBE techjobs.jobs;
--Jobs Table
--	Id: int
--	Name: longtext
--	EmployerId: int



--Part 2
--SELECT * FROM techjobs.employers WHERE location = 'St. Louis City';



--Part 3
--[1]
--SELECT distinct Id, Name, Description, js.SkillId
-- FROM techjobs.skills as ts
-- JOIN techjobs.jobskills as tjs on tjs.SkillId = ts.id
-- WHERE tjs.SkillId is not null
-- ORDER BY Name, Description ASC;
--[2]
--SELECT distinct Id, Name, Description 
--FROM techjobs.skills 
--INNER JOIN techjobs.jobskills ON jobskills.SkillId = skills.id
--WHERE jobskills.SkillId is not null
--ORDER BY Name, Description ASC;

