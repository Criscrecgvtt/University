(deffacts F (stack A B A A B B A stackA stackB))

(defrule move-to-stack-A
  (stack $?x A $?y stackA   $?z) 
  =>
  (assert (stack $?x   $?y stackA A $?z)))

(defrule move-to-stack-B
  (stack $?x B $?y stackA $?z stackB $?zz) 
  =>
  (assert (stack $?x $?y stackA $?z stackB B $?zz)))

(defrule goal
  (stack stackA $?z stackB $?zz) 
  =>
  (printout T "Solution found!" crlf)
  (halt))
