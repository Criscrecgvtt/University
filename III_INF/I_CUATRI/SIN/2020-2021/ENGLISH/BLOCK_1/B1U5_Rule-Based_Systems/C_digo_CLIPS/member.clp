(deffacts F (list A B C A B C C B A C B A))
(defrule R1
?f1 <- (list $?x1 ?y $?x2 ?y $?x3)
(test (> (length $?x2) 0))
(test (not (member ?y $?x2)))
=>
(retract ?f1)
(assert (list $?x1 ?y ?y $?x3)))
(set-strategy breadth)
(watch facts)
(watch activations)
(reset)
(run)
(exit)
