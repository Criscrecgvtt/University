(deffacts F (list 1 2 3 4))

(defrule R1
  ?f <- (list ?x $?z) =>
  (retract ?f)
  (assert (list $?z))
  (assert (element ?x)))

(defrule R2
  ?f <- (element ?x) 
        (element ?y) 
  (test (< ?x ?y)) =>
  (retract ?f)
  (assert (list-new ?x ?y)))

