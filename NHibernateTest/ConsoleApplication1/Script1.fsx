
open System;


let str = @"
SELECT id 
FROM tablea 
/* Comment with ' number 2 */ 
WHERE Name = :name 
ORDER BY Name";

let str2 = @"SELECT id FROM tablea /* Comment with ' number 2 */ WHERE Name = :name ORDER BY Name";
