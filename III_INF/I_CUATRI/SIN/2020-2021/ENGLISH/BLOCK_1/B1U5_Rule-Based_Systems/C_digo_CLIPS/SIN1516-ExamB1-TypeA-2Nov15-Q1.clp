(deffacts F (list a b a b a))
(defrule R1
  ?f <- (list ?x $?y ?x $?z) 
  =>
  (retract ?f)
  (assert (list $?y ?x $?z))
  (printout t "The list was modified" crlf))
