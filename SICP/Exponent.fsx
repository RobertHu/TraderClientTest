

let rec expt b n =  if n = 0 then 1 else b * expt b (n - 1)

let rec expt_inter b n acc = if n = 0 then acc else expt_inter b (n - 1) acc * b

let expt_inter_version b n = expt_inter b n 1

let even n = n % 2 = 0
let square x = x * x

let rec fast_expt b n = 
    if n = 0 then 1
    else
        if even n then square (fast_expt b (n / 2))
        else b * fast_expt b (n - 1)




