(deffacts F (number 5) (fact 1))
(defrule factorial
  ?f1 <- (number ?n1)
  ?f2 <- (fact ?n2)
  (test (> ?n1 1))
  =>
  (retract ?f1 ?f2)
  (assert (number (- ?n1 1)))
  (assert (fact (* ?n2 ?n1 ))))
